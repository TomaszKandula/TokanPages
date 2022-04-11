import { createTheme } from "@material-ui/core/styles";

const theme = createTheme(
{
    palette:
    {
        background: 
        {
            default: "#FFFFFF"
        }
    },
    typography: 
    {
        "fontFamily": ["Ubuntu", "sans-serif"].join(","),
        "fontSize": 14,
        "fontWeightLight": 300,
        "fontWeightRegular": 400,
        "fontWeightMedium": 500
    }
});

export default theme;
