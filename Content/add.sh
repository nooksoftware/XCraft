#!/bin/bash
for file in /path/to/images/*.png; do
  mgcb -c Content.mgcb -add "$file"
done

