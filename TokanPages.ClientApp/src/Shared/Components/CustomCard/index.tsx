import * as React from "react";
import { Link } from "react-router-dom";
import { Skeleton } from "@material-ui/lab";
import { Button, Card, CardContent } from "@material-ui/core";
import { ViewProperties } from "../../../Shared/Abstractions";

type Colour = "info" | "success" | "warning" | "error";

interface OptionsProps {
    buttonLink: string;
    buttonLabel: string;
}

interface CustomCardProps extends ViewProperties {
    caption: string;
    text: string[];
    icon: React.ReactElement;
    colour: Colour;
    linkButton?: OptionsProps;
    externalButton?: React.ReactElement;
}

const RenderIcon = (props: CustomCardProps) => {
    return React.cloneElement(props.icon, {
        ...props.icon.props,
        className: `custom-card-icon vertical-centre alert-${props.colour}`,
    });
};

const RenderLinkButton = (props: CustomCardProps): React.ReactElement => {
    return props.isLoading ? (
        <Skeleton variant="rect" width="100%" height="40px" />
    ) : (
        <div className="mt-48">
            <Link to={props.linkButton?.buttonLink ?? ""} className="link" rel="noopener nofollow">
                <Button fullWidth variant="contained" className="button" disabled={props.isLoading}>
                    {props.linkButton?.buttonLabel}
                </Button>
            </Link>
        </div>
    );
};

const RenderExternalButton = (props: CustomCardProps): React.ReactElement => {
    return props.isLoading ? (
        <Skeleton variant="rect" width="100%" height="40px" />
    ) : (
        <div className="mt-48">{props.externalButton}</div>
    );
};

export const CustomCard = (props: CustomCardProps): React.ReactElement => {
    return (
        <Card elevation={0} className="card">
            <CardContent className="card-content">
                <div className="custom-card-background mt-25 mb-25">
                    <div className="custom-card-icon-holder vertical-centre">
                        <RenderIcon {...props} />
                    </div>
                </div>
                <h2 className="custom-card-caption text-centre">
                    {props.isLoading ? <Skeleton variant="text" /> : props.caption}
                </h2>
                <div className="mt-48">
                    {props.text.map((value: string, index: number) => (
                        <div className="custom-card-text text-centre" key={index}>
                            {props.isLoading ? <Skeleton variant="text" /> : value}
                        </div>
                    ))}
                </div>
                {props.linkButton ? <RenderLinkButton {...props} /> : null}
                {props.externalButton ? <RenderExternalButton {...props} /> : null}
                <div className={props.linkButton || props.externalButton ? "mb-25" : "mb-48"}></div>
            </CardContent>
        </Card>
    );
};
