#!/bin/bash

#Define source and destination directories


SOURCE_DIR="~/Dev/XCraft/XCraftLib"
DEST_DIR="~/Dev/XCraftLib"

# Expand the tilde (~) to the full home directory path
SOURCE_DIR=$(eval echo "$SOURCE_DIR")
DEST_DIR=$(eval echo "$DEST_DIR")

# Check if the directories exist
if [ ! -d "$SOURCE_DIR" ]; then
    echo "Source directory does not exist."
    exit 1
fi

if [ ! -d "$DEST_DIR" ]; then
    echo "error2"
    exit 1
fi
# Replace files
for FILE in "$SOURCE_DIR"/*; do
    BASENAME=$(basename "$FILE") # Get file name without the path
    DEST_FILE="$DEST_DIR/$BASENAME"

    # Copy and replace file in the destination
    cp "$FILE" "$DEST_FILE"
    echo "Replaced $DEST_FILE with $FILE"
done

cd ~/Dev/XCraftLib/

git add . && git commit -m "." && git push

cd ~/Dev/XCraft/XCraftLib/

git pull && cd ~/Dev/XCraft

git add . && git commit -m "Library retransform, and possibly changes in game's code" && git push