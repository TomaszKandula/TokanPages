import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles(() => (
{
    appBar:
    {
        background: "#1976D2"
    },
    toolBar: 
    { 
        justifyContent: "center", 
    },
    mainLogo:
    {
        width: 210,
    },
    mainLink:
    {
        marginTop: "10px",
        variant:"h5", 
        color: "inherit", 
        underline: "none"
    }
}));

export default useStyles;
