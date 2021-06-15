import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../Theme/customColours";

const unsubscribeStyle = makeStyles(() => (
{
    section:
    {
        backgroundColor: CustomColours.background.white
    },
    card:
    {
        marginTop: 10,
        marginLeft: 15,
        marginRight: 15,
        marginBottom: 10
    }
}));

export default unsubscribeStyle;
