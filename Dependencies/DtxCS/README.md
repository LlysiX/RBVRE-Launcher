# DtxCS
A C# library to parse and interpret the data-driven scripting language used in Harmonix games

"DTX" is used to refer to the .dta/.dtb/.dtx/.\*\_dta\_\* scripting environment that's used in most of Harmonix's games, including the Rock Band and Guitar Hero series.

This library provides an interface to load these scripts and use them from a C# environment. This fork aims to continue to improve on maxton's efforts.

## DTXTool

A command-line utility called DTXTool is provided as a way to convert and/or (en/de)crypt DTX format files.

## Supported Features
- Read data from plaintext DTA or serialized/encrypted DTB format
- Execute script commands (some builtins, such as for basic arithmetic, are included)
- Export/view plaintext DTA from root array
- Directive support (#merge, #include, #ifdef, #define, etc) (WIP)
- Re-serializing to DTB (WIP)

## Not yet supported
- Variables
- Function definitions / variable capture / Dynamic scoping
- Object binding / message passing system