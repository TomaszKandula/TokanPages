import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import GitHubIcon from "@material-ui/icons/GitHub";
import LinkedInIcon from "@material-ui/icons/LinkedIn";
import WarningIcon from '@material-ui/icons/Warning';
import { IFooterContentIconDto } from "../../Api/Models";
import { MediumIcon } from "../../Theme/Icons/mediumIcon";
import footerStyle from "./Styles/footerStyle";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    terms: string;
    policy: string;
    versionInfo: string;
    hasVersionInfo: boolean;
    backgroundColor: string;
    boxPaddingBottom: number;
    copyright: string;
    reserved: string;
    icons: IFooterContentIconDto[];
}

export default function FooterView(props: IBinding) 
{
    const SetTermsLink = (): JSX.Element => 
    { 
        return (
            <Link to="/terms" className={classes.links}>
                {props.bind?.terms}
            </Link>
        ); 
    };

    const SetPolicyLink = (): JSX.Element => 
    { 
        return (
            <Link to="/policy" className={classes.links}>
                {props.bind?.policy}
            </Link>
        );
    };

    const RenderIcon = (iconName: string): JSX.Element => 
    {
        switch(iconName)
        {
            case "GitHubIcon": return <GitHubIcon />;
            case "LinkedInIcon": return <LinkedInIcon />;
            case "MediumIcon": return <MediumIcon />;
            default: return <WarningIcon />;
        }
    };

    const RenderVersionInfo = (): JSX.Element =>
    {
        const applicationVersionInfo = 
            <Box pt={1} pb={6} display="flex"  justifyContent="center" alignItems="center">
                <Typography component="p" className={classes.version}>
                    {props.bind?.versionInfo}
                </Typography>
            </Box>;
        
        return props.bind?.hasVersionInfo 
            ? <div></div> 
            : applicationVersionInfo
    };
    
    const classes = footerStyle();
    return (
        <footer className={classes.root} style={{ backgroundColor: props.bind?.backgroundColor }} >
            <Container maxWidth="lg">
                <div data-aos="zoom-in">
                    <Box pt={6} pb={props.bind?.boxPaddingBottom} display="flex" flexWrap="wrap" alignItems="center">
                        <Typography component="p" gutterBottom={false} className={classes.copy}>
                            {props.bind?.copyright} | {props.bind?.reserved} | <SetTermsLink /> | <SetPolicyLink />
                        </Typography>
                        <Box ml="auto" className={classes.iconsBoxRoot}>
                            {props.bind?.icons?.map((item: IFooterContentIconDto, index: number) => (
                                <IconButton 
                                    key={index}
                                    color="default" 
                                    aria-label={item.name} 
                                    href={item.link} 
                                    target="_blank">
                                    {RenderIcon(item.name)}
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
