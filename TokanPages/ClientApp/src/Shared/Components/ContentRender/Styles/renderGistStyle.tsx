import { makeStyles } from "@material-ui/core";

const renderGistStyle = makeStyles(() => (
{
    card:
    {
        borderRadius: 0
    },
    syntaxHighlighter:
    {
        marginTop: "0!important" as "0",
        marginBottom: "0!important" as "0",
        backgroundColor: "white!important" as "white",
        fontSize: "12px"
    }
}));    

export default renderGistStyle;
