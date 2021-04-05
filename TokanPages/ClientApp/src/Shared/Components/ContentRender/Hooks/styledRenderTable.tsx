import { 
    createStyles, 
    makeStyles, 
    withStyles, 
    TableCell, 
    TableRow
} from "@material-ui/core";
import { CustomColours } from "Theme/customColours";

const StyledTableCell = withStyles(() => createStyles(
{
    head: 
    {
        backgroundColor: CustomColours.background.gray1,
        color: CustomColours.typography.white,
        fontSize: 18,
        fontWeight: "bold"
    },
    body: 
    {
        fontSize: 15,
    },
}),
)(TableCell);

const StyledTableRow = withStyles(() => createStyles(
{
    root: 
    {
        '&:nth-of-type(odd)': 
        {
            backgroundColor: CustomColours.background.lightGray2,
        },
    },
}),
)(TableRow);

const useStyles = makeStyles(() => (
{
    table:
    {
        minWidth: 650
    },
    header:
    {
        textAlign: "left"
    },
    row:
    {
        textAlign: "left"
    }
}));

export { useStyles, StyledTableRow, StyledTableCell };
