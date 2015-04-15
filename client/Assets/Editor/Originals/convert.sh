#!/bin/sh

if [[ $# -ne 2 ]]; then
  echo "Usage: convert.sh [dpi] [input]"
  exit
fi

/Applications/Inkscape.app/Contents/Resources/bin/inkscape --export-png="../../Resources/Sprites/dpi$1/$2.png" --export-dpi="$1" --export-background-opacity=0 --without-gui "$2.svg"
