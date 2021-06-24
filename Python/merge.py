import glob


if __name__ == "__main__":
    # Combine separated files into one
    csvs = glob.glob("./csv/*.csv")
    csvs.sort()
    print(csvs)

    csv_combined = open("./attitude.csv", "w")
    csv_combined.writelines("frame,w,x,y,z,euler_x,euler_y,euler_z\n")

    for item in csvs:
        with open(item, "r") as f:
            lines = f.readlines()
            csv_combined.writelines(lines[1:])

    csv_combined.close()
