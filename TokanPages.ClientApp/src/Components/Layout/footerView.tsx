import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import { GetIcon } from "../../Shared/Components/GetIcon/getIcon";
import { IFooterContentIconDto } from "../../Api/Models";
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
    copyright: string;
    reserved: string;
    icons: IFooterContentIconDto[];
}

const FooterView = (props: IBinding): JSX.Element => 
{
    const SetTermsLink = (): JSX.Element => 
    { 
        return (<Link to="/terms" className={classes.links}>
            {props.bind?.terms}
        </Link>); 
    };

    const SetPolicyLink = (): JSX.Element => 
    { 
        return (<Link to="/policy" className={classes.links}>
            {props.bind?.policy}
        </Link>);
    };

    const RenderIconButtons = (): JSX.Element =>
    {
        const icons = 
        <Box ml="auto" className={classes.icon_box} data-aos="zoom-in">
            {props.bind?.icons?.map((item: IFooterContentIconDto, index: number) => 
            (<IconButton 
                className={classes.icon}
                aria-label={item.name} 
                href={item.link} 
                key={index}
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

    const classes = footerStyle();
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

export default FooterView;
