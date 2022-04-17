# Soil Params

Tool to calculate soil hydraulic parameters through several models

## Usage

1. You have to make an input file named `input.json`. The following listing gives an example:

```json
[
    {
        "Title": "Soil from location X",
        "Description": "Samples collected in 02/03/2022",
        "PressureHeads": [
            0, 5, 30, 100, 300, 500, 1000, 5000, 10000, 15000
        ],
        "MeasuredWaterContents": [
            0.543, 0.474, 0.402, 0.390, 0.327, 0.309, 0.290, 0.287, 0.286, 0.280
        ],
        "Models": [
            "VG", "BC"
        ]
    },
    {
        "Title": "Soil from location Y",
        "Description": "Samples collected in 09/03/2022",
        "PressureHeads": [
            0, 5, 30, 100, 300, 500, 1000, 5000, 10000, 15000
        ],
        "MeasuredWaterContents": [
            0.543, 0.474, 0.402, 0.390, 0.327, 0.309, 0.290, 0.287, 0.286, 0.280
        ],
        "Models": [
            "VG"
        ]
    }
]
```

In this file you configure the input parameters for the application. It's quite intuitive, I think. You give informations such as:
- Title: A meaningfull title for a group of sample (usually collected from a specific site)
- Description: It's self explanatory =)
- PressureHeads: A list of the pressure heads (tension) that was applied in the samples
- MeasuredWaterContents: A list of the water contents found after applied pressure head
- Models: The model names on which you want to estimate the parameters. Options are:
    - VG: van Genuchten (1980) model
    - BC: Brooks and Corey (1964) model

2. Run the command `soilparams input.json output.txt`.

It will generate a file called `output.txt` with the results.