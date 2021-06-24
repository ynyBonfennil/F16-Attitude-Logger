import csv

if __name__ == "__main__":
    with open("./0-.csv", "w", newline="") as f:
        writer = csv.writer(f)
        writer.writerow(["frame", "w", "x", "y", "z", "euler_x", "euler_y", "euler_z"])
        for i in range(2007):
            writer.writerow([i,1,0,0,0,0,0,0])
