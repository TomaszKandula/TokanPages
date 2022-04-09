import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../Theme/customColours";

const activateAccountStyle = makeStyles(() => (
{
    section:
    {
        backgroundColor: CustomColours.colours.white
    },
    card:
    {
        marginTop: 10,
        marginLeft: 15,
        marginRight: 15,
        marginBottom: 10
    }
}));

export default activateAccountStyle;
