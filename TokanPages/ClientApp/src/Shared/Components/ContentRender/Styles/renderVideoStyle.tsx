import { makeStyles } from "@material-ui/core";
import { CustomColours } from "Theme/customColours";

const renderVideoStyle = makeStyles(() => (
{
    card:
    {
        borderRadius: 0,
        marginTop: "40px",
        marginBottom: "40px"
    },
    image:
    {
        cursor: "pointer"
    },
    text:
    {
        color: CustomColours.typography.gray1,
        paddingTop: "1px",
        paddingBottom: "1px",
        paddingLeft: "10px",
        paddingRight: "10px",
        lineHeight: 1.8
    }
}));

export default renderVideoStyle;
