import { Colours } from "../../../../Theme";
import { 
    createStyles, 
    withStyles, 
    TableCell, 
    TableRow
} from "@material-ui/core";

export const CustomTableCell = withStyles(() => createStyles(
{
    head: 
    {
        backgroundColor: Colours.colours.gray1,
        color: Colours.colours.white,
        fontSize: 18,
        fontWeight: "bold"
    },
    body: 
    {
        fontSize: 15,
    },
}),
)(TableCell);

export const CustomTableRow = withStyles(() => createStyles(
{
    root: 
    {
        "&:nth-of-type(odd)": 
        {
            backgroundColor: Colours.colours.lightGray2,
        },
    },
}),
)(TableRow);
