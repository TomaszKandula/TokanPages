import * as React from "react";
import { Card, CardContent } from "@material-ui/core";

interface CustomCardProps {
    caption: string;
    text: string;
    icon: React.ReactElement;
}

const RenderIcon = (props: CustomCardProps) => {
    return React.cloneElement(props.icon, { 
        ...props.icon.props, 
        className: "custom-card-icon vertical-centre" 
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
                <h2 className="custom-card-caption text-centre mb-32">
                    {props.caption}
                </h2>
                <div className="custom-card-text text-centre mb-32">
                    {props.text}
                </div>
            </CardContent>
        </Card>
    );
}
