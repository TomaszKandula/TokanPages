import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import GitHubIcon from "@material-ui/icons/GitHub";
import LinkedInIcon from "@material-ui/icons/LinkedIn";
import WarningIcon from '@material-ui/icons/Warning';
import { CustomColours } from "../../Theme/customColours";
import { IFooterContentDto, IFooterContentIconDto } from "../../Api/Models";
import { MediumIcon } from "../../Theme/Icons/medium";
import validate from "validate.js";
import useStyles from "./Hooks/styleFooter";

export default function Footer(props: { footer: IFooterContentDto, isLoading: boolean, backgroundColor?: string | undefined }) 
{
    const padingBottomLarge: number = 6;
    const paddingBottomSmall: number = 1;
    
    const versionDateTime: string = process.env.REACT_APP_VERSION_DATE_TIME ?? "";
    const versionNumber: string = process.env.REACT_APP_VERSION_NUMBER ?? "";
    const versionInfo: string = `Version ${versionNumber} (${versionDateTime})`;

    const hasVersionInfo = validate.isEmpty(versionNumber) && validate.isEmpty(versionDateTime);

    const classes = useStyles();
    const backgroundColor: string = !props.backgroundColor 
        ? CustomColours.background.lightGray1 
        : props.backgroundColor as string;

    const boxPaddingBottom: number = hasVersionInfo 
        ? padingBottomLarge 
        : paddingBottomSmall;

    const SetTermsLink = (): JSX.Element => 
    { 
        return (
            <Link to="/terms" className={classes.links}>
                {props.footer?.content.terms}
            </Link>
        ); 
    };

    const SetPolicyLink = (): JSX.Element => 
    { 
        return (
            <Link to="/policy" className={classes.links}>
                {props.footer?.content.policy}
            </Link>
        );
    };

    const RenderIcon = (props: { iconName: string }): JSX.Element => 
    {
        let icon: JSX.Element = <></>;
        switch(props.iconName)
        {
            case "GitHubIcon": icon = <GitHubIcon />; break;
            case "LinkedInIcon": icon = <LinkedInIcon />; break;
            case "MediumIcon": icon = <MediumIcon />; break;
            default: icon = <WarningIcon />;
        }

        return icon;
    };

    const RenderVersionInfo = (): JSX.Element =>
    {
        const applicationVersionInfo = 
            <Box pt={1} pb={6} display="flex"  justifyContent="center" alignItems="center">
                <Typography component="p" className={classes.version}>
                    {versionInfo}
                </Typography>
            </Box>;
        
        return hasVersionInfo 
            ? <div></div> 
            : applicationVersionInfo
    };

    return (
        <footer className={classes.root} style={{ backgroundColor: backgroundColor }} >
            <Container maxWidth="lg">
                <div data-aos="zoom-in">
                    <Box pt={6} pb={boxPaddingBottom} display="flex" flexWrap="wrap" alignItems="center">
                        <Typography component="p" gutterBottom={false} className={classes.copy}>
                            {props.footer?.content.copyright} | {props.footer?.content.reserved} | <SetTermsLink /> | <SetPolicyLink />
                        </Typography>
                        <Box ml="auto" className={classes.iconsBoxRoot}>
                            {props.footer?.content.icons.map((item: IFooterContentIconDto, index: number) => (
                                <IconButton 
                                    key={index}
                                    color="default" 
                                    aria-label={item.name} 
                                    href={item.link} 
                                    target="_blank">
                                    <RenderIcon iconName={item.name} />
                                </IconButton>
                            ))}
                        </Box>
                    </Box>
                    <RenderVersionInfo />
                </div>
            </Container>
        </footer>
    );
}
