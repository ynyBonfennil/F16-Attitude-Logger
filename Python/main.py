import glob
import pandas as pd
import numpy as np
import quaternion
from scipy.interpolate import interp1d
from scipy.signal import butter, lfilter

ATTITUDE_COMBINED_CSV = "./attitude_combined.csv"
ATTITUDE_NO_DUPLICATES_CSV = "./attitude_no_duplicates.csv"
ATTITUDE_CSV = "./attitude.csv"

def butter_lowpass(cutoff, fs, order=5):
    nyq = 0.5*fs
    normal_cutoff = cutoff /nyq
    b, a = butter(order, normal_cutoff, btype="low", analog=False)
    return b, a

def butter_lowpass_filter(data, cutoff, fs, order=5):
    b, a = butter_lowpass(cutoff, fs, order=order)
    y = lfilter(b, a, data)
    return y


if __name__ == "__main__":
    # Combine separated files into one
    logs = glob.glob("./csv/*.csv")
    logs.sort()

    log_combined = open(ATTITUDE_COMBINED_CSV, "w")
    log_combined.writelines("frame,w,x,y,z,euler_x,euler_y,euler_z\n")

    for item in logs:
        with open(item, "r") as f:
            lines = f.readlines()
            log_combined.writelines(lines[1:])

    log_combined.close()

    # Drop duplicates
    df = pd.read_csv(ATTITUDE_COMBINED_CSV,
                        header=0,
                        dtype={
                            "frame": int,
                            "w": float,
                            "x": float,
                            "y": float,
                            "z": float,
                            "euler_x": float,
                            "euler_y": float,
                            "euler_z": float,
                        })
    
    df.drop_duplicates(subset="frame", keep="last", inplace=True)
    df.to_csv(ATTITUDE_NO_DUPLICATES_CSV, index=False)
    
    # Remove offset
    # First several frames must have no rotation,
    # but it seems it has euler_x, euler_y, euler_z = 6.16, 16, 0.373
    # Hence we remove it from all the rotation
    frame = np.array(df["frame"])
    w = np.array(df["w"])
    x = np.array(df["x"])
    y = np.array(df["y"])
    z = np.array(df["z"])

    offset = np.quaternion(w[0], x[0], y[0], z[0])
    offset_inv = offset.inverse()
    for i, _ in enumerate(frame):
        quat = np.quaternion(w[i], x[i], y[i], z[i]) * offset_inv
        w[i] = quat.w
        x[i] = quat.x
        y[i] = quat.y
        z[i] = quat.z
    
    # Interpolate and apply low pass filter
    fw = interp1d(frame, w, kind="cubic")
    fx = interp1d(frame, x, kind="cubic")
    fy = interp1d(frame, y, kind="cubic")
    fz = interp1d(frame, z, kind="cubic")

    frame_sequence = np.linspace(150, 17939, num=17940-150, endpoint=True)
    order = 6
    fs = 30.0
    cutoff = 1
    fw_butter = butter_lowpass_filter(fw(frame_sequence), cutoff, fs, order)
    fx_butter = butter_lowpass_filter(fx(frame_sequence), cutoff, fs, order)
    fy_butter = butter_lowpass_filter(fy(frame_sequence), cutoff, fs, order)
    fz_butter = butter_lowpass_filter(fz(frame_sequence), cutoff, fs, order)
    
    # Export
    # First 150 frames have no rotation, so add them manually
    with open(ATTITUDE_CSV, "w") as f:
        f.write("frame,w,x,y,z\n")
        for frame in range(150):
            f.write("{0},{1},{2},{3},{4}\n".format(int(frame), 1, 0, 0, 0))
        for i, frame in enumerate(frame_sequence):
            f.write("{0},{1},{2},{3},{4}\n".format(int(frame), fw_butter[i], fx_butter[i], fy_butter[i], fz_butter[i]))
