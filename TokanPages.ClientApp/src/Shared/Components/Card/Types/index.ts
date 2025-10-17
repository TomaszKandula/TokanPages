import { TColour, ViewProperties } from "../../../../Shared/Types";

export interface OptionsProps {
    buttonLink: string;
    buttonLabel: string;
}

export interface CustomCardProps extends ViewProperties {
    caption: string;
    text: string[];
    icon: React.ReactElement;
    colour: TColour;
    linkButton?: OptionsProps;
    externalButton?: React.ReactElement;
    externalContent?: React.ReactElement;
}
