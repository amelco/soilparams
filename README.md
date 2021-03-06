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
            "VG"
        ],
        "InitialGuess": [
            0.1, 0.5, 1.2, 0.5
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
        ],
        "InitialGuess": [
            0.1, 0.5, 1.2, 0.5
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
- InitialGuess: A list of initial guess values for the model parameters to be estimated
  - For VG model, parameter order is: theta_r, theta_s, alpha, n

2. Run the command `soilparams input.json`.

Alternatively, you can run just the command `soilparams` and it will try to open the default value for the input file (`input.json`).
The statistics will be printed in the `statistics.txt` file.

Also, it will print on the screen the output with the parameters, standard deviation and standard error.

### To do

- Account for several models (and initial guesses) per item
- Store a more detailed output in an *easy-to-read* file
- Generate descriptive statistics
- Generate statistical analysis of the result
- Generate WRC plot
- Account for repetition in MeasuredWaterContents
