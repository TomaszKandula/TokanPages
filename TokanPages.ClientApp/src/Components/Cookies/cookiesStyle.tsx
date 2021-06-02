import { makeStyles } from "@material-ui/core";

const cookiesStyle = makeStyles(() => (
{
    open:
    {
        visibility: "visible",
        opacity: 1
    },
    close:
    {
        transition: "0.3s all ease",
        opacity: 0,
        visibility: "hidden"
    }
}));

export default cookiesStyle;
