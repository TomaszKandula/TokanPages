import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import { GetIcon } from "../../../../Shared/Components/GetIcon/getIcon";
import { IIcon, ILink } from "../../../../Api/Models";
import { FooterStyle } from "./footerStyle";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    terms: ILink;
    policy: ILink;
    versionInfo: string;
    hasVersionInfo: boolean;
    backgroundColor: string;
    copyright: string;
    reserved: string;
    icons: IIcon[];
}

export const FooterView = (props: IBinding): JSX.Element => 
{
    const SetTermsLink = (): JSX.Element => 
    { 
        if (Validate.isEmpty(props.bind?.terms?.href))
        {
            return(<>{props.bind?.terms?.text}</>);
        }

        return(
            <Link to={props.bind?.terms?.href} className={classes.links}>
                {props.bind?.terms?.text}
            </Link>); 
    };

    const SetPolicyLink = (): JSX.Element => 
    { 
        if (Validate.isEmpty(props.bind?.policy?.href))
        {
            return(<>{props.bind?.policy?.text}</>);
        }

        return (
            <Link to={props.bind?.policy?.href} className={classes.links}>
                {props.bind?.policy?.text}
            </Link>);
    };

    const RenderIconButtons = (): JSX.Element =>
    {
        const icons = 
        <Box ml="auto" className={classes.icon_box} data-aos="zoom-in">
            {props.bind?.icons?.map((item: IIcon, _index: number) => 
            (<IconButton 
                className={classes.icon}
                aria-label={item.name} 
                href={item.href} 
                key={uuidv4()}
                color="default" 
                target="_blank">
                <GetIcon iconName={item.name} />
            </IconButton>))}
        </Box>;

        return icons;
    }

    const RenderCopyrightBar = (): JSX.Element => 
    {
        return (<Box pt={6} pb={1} className={classes.copyright_box}>
            <Typography className={classes.copyright} data-aos="zoom-in">
                {props.bind?.copyright} | {props.bind?.reserved} | <SetTermsLink /> | <SetPolicyLink />
            </Typography>
            <RenderIconButtons />
        </Box>);
    }

    const RenderVersionInfo = (): JSX.Element | null =>
    {
        const applicationVersionInfo = 
            <Box display="flex"  justifyContent="center" alignItems="center" data-aos="zoom-in">
                <Typography className={classes.version}>
                    {props.bind?.versionInfo}
                </Typography>
            </Box>;

        return props.bind?.hasVersionInfo ? null : applicationVersionInfo
    };

    const classes = FooterStyle();
    return (
        <footer className={classes.page_footer}>
            <Container maxWidth="lg">
                <RenderCopyrightBar />
                <RenderVersionInfo />
                <Box pb={15}></Box>
            </Container>
        </footer>
    );
}
