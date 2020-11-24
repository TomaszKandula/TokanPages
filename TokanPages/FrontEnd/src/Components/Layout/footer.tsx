import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import GitHubIcon from "@material-ui/icons/GitHub";
import LinkedInIcon from "@material-ui/icons/LinkedIn";
import useStyles from "./Hooks/styleFooter";

export default function Footer(props: { backgroundColor?: string }) 
{
  
    const classes = useStyles();
    const content = 
    {
        terms: "Terms of use",
        policy: "Privacy policy",
        copyright: "© 2020 Tomasz Kandula",
        reserved: "All rights reserved"
    };

    const SetTermsLink = () => { return (<Link to="/terms" className={classes.links}>{content.terms}</Link>); }
    const SetPolicyLink = () => { return (<Link to="/policy" className={classes.links}>{content.policy}</Link>); }

    return (
        <footer className={classes.root} style={{ backgroundColor: props.backgroundColor }} >
            <Container maxWidth="lg">
                <div data-aos="zoom-in">
                    <Box py={6} display="flex" flexWrap="wrap" alignItems="center">
                        <Typography component="p" gutterBottom={false} className={classes.copy}>
                            {content.copyright} | {content.reserved} | <SetTermsLink /> | <SetPolicyLink />
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
