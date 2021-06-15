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
        "fontFamily": `charter, Georgia, Cambria, "Times New Roman", Times, serif`,
        "fontSize": 14,
        "fontWeightLight": 300,
        "fontWeightRegular": 400,
        "fontWeightMedium": 500
    }
});

export default theme;
