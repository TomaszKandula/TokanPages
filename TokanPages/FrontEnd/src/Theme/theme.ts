import { createMuiTheme } from '@material-ui/core/styles';

const theme = createMuiTheme(
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
        "fontFamily": `"Roboto", "Helvetica", "Arial", sans-serif`,
        "fontSize": 14,
        "fontWeightLight": 300,
        "fontWeightRegular": 400,
        "fontWeightMedium": 500
    }
});

export default theme;

