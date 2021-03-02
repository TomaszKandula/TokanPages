import { 
    createStyles, 
    makeStyles, 
    withStyles, 
    TableCell, 
    TableRow, 
    Theme 
} from "@material-ui/core";

const StyledTableCell = withStyles((theme: Theme) => createStyles(
{
    head: 
    {
        backgroundColor: "#1976D2",
        color: theme.palette.common.white,
        fontSize: 18,
        fontWeight: "bold"
    },
    body: 
    {
        fontSize: 15,
    },
}),
)(TableCell);

const StyledTableRow = withStyles((theme: Theme) => createStyles(
{
    root: 
    {
        '&:nth-of-type(odd)': 
        {
            backgroundColor: theme.palette.action.hover,
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
