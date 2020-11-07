import React from "react";
import { Link } from "react-router-dom";
import { makeStyles } from "@material-ui/core/styles";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import GitHubIcon from "@material-ui/icons/GitHub";
import LinkedInIcon from "@material-ui/icons/LinkedIn";

const useStyles = makeStyles((theme) => (
{
    root: 
    {
        [theme.breakpoints.down("md")]: 
        {
            textAlign: "center"
        },
    },
    iconsBoxRoot: 
    {
        [theme.breakpoints.down("md")]: 
        {
            width: "100%",
            marginBottom: theme.spacing(0),
        }
    }, 
    copy: 
    {
        color: "#757575",
        [theme.breakpoints.down("md")]: 
        {
            width: "100%",
            order: 12,
        }
    },
    links:
    {
        color: "#757575",
        textDecoration: "none"
    }
}));

export default function Footer(props: { backgroundColor?: string }) 
{
  
    const classes = useStyles();

    const SetTermsLink = () => 
    {
        return (<Link to="/terms" className={classes.links}>Terms of use</Link>);
    }

    const SetPolicyLink = () => 
    {
        return (<Link to="/policy" className={classes.links}>Privacy policy</Link>);
    }

    return (
        <footer className={classes.root} style={{ backgroundColor: props.backgroundColor }} >
            <Container maxWidth="lg">
                <div data-aos="zoom-in">
                    <Box py={6} display="flex" flexWrap="wrap" alignItems="center">
                        <Typography component="p" gutterBottom={false} className={classes.copy}>
                            © 2020 Tomasz Kandula | All rights reserved | <SetTermsLink /> | <SetPolicyLink />
                        </Typography>
                        <Box ml="auto" className={classes.iconsBoxRoot}>
                            <IconButton color="default" aria-label="GitHub" href="https://github.com/TomaszKandula" target="_blank">
                                <GitHubIcon />
                            </IconButton>
                            <IconButton color="default" aria-label="LinkedIn" href="https://www.linkedin.com/in/tomaszkandula/" target="_blank">
                                <LinkedInIcon />
                            </IconButton>
                        </Box>
                    </Box>
                </div>
            </Container>
        </footer>
    );

}
