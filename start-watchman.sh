
watchman watch D:/Unity/Breakout
while true
do 
    watchman -- trigger D:/Unity/Breakout auto-commit '*' -- ./auto-commit.sh
    sleep 1200
done
