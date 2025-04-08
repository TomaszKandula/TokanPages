import * as React from "react";
import { Card, CardContent } from "@material-ui/core";
import { Skeleton } from "@material-ui/lab";
import { ViewProperties } from "../../../Shared/Abstractions";

type Colour = "info" | "success" | "warning" | "error";

interface CustomCardProps extends ViewProperties {
    caption: string;
    text: string[];
    icon: React.ReactElement;
    colour: Colour;
}

const RenderIcon = (props: CustomCardProps) => {
    return React.cloneElement(props.icon, { 
        ...props.icon.props, 
        className: `custom-card-icon vertical-centre alert-${props.colour}` 
    });
}

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
                {props.text.map((value: string, index: number) => (
                    <div className="custom-card-text text-centre" key={index}>
                        {props.isLoading ? <Skeleton variant="text" /> : value}
                    </div>
                ))}
                <div className="mb-32"></div>
            </CardContent>
        </Card>
    );
}
