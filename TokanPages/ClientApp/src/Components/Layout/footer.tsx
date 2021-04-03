import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import GitHubIcon from "@material-ui/icons/GitHub";
import LinkedInIcon from "@material-ui/icons/LinkedIn";
import { CustomColours } from "../../Theme/customColours";
import { IFooterContentDto } from "../../Api/Models";
import useStyles from "./Hooks/styleFooter";

export default function Footer(props: { footer: IFooterContentDto, isLoading: boolean, backgroundColor?: string | undefined }) 
{
    const classes = useStyles();
    const backgroundColor: string = !props.backgroundColor 
        ? CustomColours.background.lightGray1 
        : props.backgroundColor as string;

    const SetTermsLink = () => 
    { 
        return (
            <Link to="/terms" className={classes.links}>
                {props.footer.content.terms}
            </Link>
        ); 
    };

    const SetPolicyLink = () => 
    { 
        return (
            <Link to="/policy" className={classes.links}>
                {props.footer.content.policy}
            </Link>
        );
    };

    return (
        <footer className={classes.root} style={{ backgroundColor: backgroundColor }} >
            <Container maxWidth="lg">
                <div data-aos="zoom-in">
                    <Box py={6} display="flex" flexWrap="wrap" alignItems="center">
                        <Typography component="p" gutterBottom={false} className={classes.copy}>
                            {props.footer.content.copyright} | {props.footer.content.reserved} | <SetTermsLink /> | <SetPolicyLink />
                        </Typography>
                        <Box ml="auto" className={classes.iconsBoxRoot}>
                            <IconButton 
                                color="default" 
                                aria-label={props.footer.content.icons.firstIcon.name} 
                                href={props.footer.content.icons.firstIcon.link} 
                                target="_blank">
                                <GitHubIcon />
                            </IconButton>
                            <IconButton 
                                color="default" 
                                aria-label={props.footer.content.icons.secondIcon.name} 
                                href={props.footer.content.icons.secondIcon.link} 
                                target="_blank">
                                <LinkedInIcon />
                            </IconButton>
                        </Box>
                    </Box>
                </div>
            </Container>
        </footer>
    );
}
