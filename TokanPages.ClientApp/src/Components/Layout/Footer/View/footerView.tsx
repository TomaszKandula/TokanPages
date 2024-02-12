import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import { GetIcon } from "../../../../Shared/Components/GetIcon/getIcon";
import { IconDto, LinkDto } from "../../../../Api/Models";
import { FooterStyle } from "./footerStyle";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";

interface Properties {
    terms: LinkDto;
    policy: LinkDto;
    versionInfo: string;
    hasVersionInfo: boolean;
    backgroundColor: string;
    copyright: string;
    reserved: string;
    icons: IconDto[];
}

const SetTermsLink = (props: Properties): JSX.Element => {
    const classes = FooterStyle();

    if (Validate.isEmpty(props.terms?.href)) {
        return <>{props.terms?.text}</>;
    }

    return (
        <Link to={props.terms?.href} className={classes.links}>
            {props.terms?.text}
        </Link>
    );
};

const SetPolicyLink = (props: Properties): JSX.Element => {
    const classes = FooterStyle();

    if (Validate.isEmpty(props.policy?.href)) {
        return <>{props.policy?.text}</>;
    }

    return (
        <Link to={props.policy?.href} className={classes.links}>
            {props.policy?.text}
        </Link>
    );
};

const RenderIconButtons = (props: Properties): JSX.Element => {
    const classes = FooterStyle();
    const icons = (
        <Box ml="auto" className={classes.icon_box} data-aos="zoom-in">
            {props.icons?.map((item: IconDto, _index: number) => (
                <IconButton
                    className={classes.icon}
                    aria-label={item.name}
                    href={item.href}
                    key={uuidv4()}
                    color="default"
                    target="_blank"
                >
                    <GetIcon iconName={item.name} />
                </IconButton>
            ))}
        </Box>
    );

    return icons;
};

const RenderCopyrightBar = (props: Properties): JSX.Element => {
    const classes = FooterStyle();
    return (
        <Box pt={6} pb={1} className={classes.copyright_box}>
            <Typography className={classes.copyright} data-aos="zoom-in">
                {props.copyright} | {props.reserved} | <SetTermsLink {...props} /> | <SetPolicyLink {...props} />
            </Typography>
            <RenderIconButtons {...props} />
        </Box>
    );
};

const RenderVersionInfo = (props: Properties): JSX.Element | null => {
    const classes = FooterStyle();
    const applicationVersionInfo = (
        <Box display="flex" justifyContent="center" alignItems="center" data-aos="zoom-in">
            <Typography className={classes.version}>{props.versionInfo}</Typography>
        </Box>
    );

    return props.hasVersionInfo ? null : applicationVersionInfo;
};

export const FooterView = (props: Properties): JSX.Element => {
    const classes = FooterStyle();
    return (
        <footer className={classes.page_footer}>
            <Container maxWidth="lg">
                <RenderCopyrightBar {...props} />
                <RenderVersionInfo {...props} />
                <Box pb={15}></Box>
            </Container>
        </footer>
    );
};
