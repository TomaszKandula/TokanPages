import { makeStyles } from "@material-ui/core/styles";

export const ScrollToTopStyle = makeStyles(theme => ({
    scrollToTop: {
        position: "fixed",
        bottom: theme.spacing(2),
        right: theme.spacing(2),
    },
}));
