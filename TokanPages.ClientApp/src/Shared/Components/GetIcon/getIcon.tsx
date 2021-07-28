import * as React from "react";
import { 
    Apple, 
    Assignment, 
    Build, 
    ContactMail, 
    DirectionsBike, 
    Gavel, 
    Home, 
    MusicNote, 
    Person, 
    PersonAdd, 
    PhotoCamera, 
    Policy, 
    Star, 
    Subject,
    SportsSoccer, 
    ViewList 
} from "@material-ui/icons";

interface IProperty
{
    iconName: string;
}

export function GetIcon(props: IProperty): JSX.Element
{
    let renderIcon: JSX.Element;
    switch(props.iconName)
    {
        case "Person":
            renderIcon = <Person />;
            break;
        case "PersonAdd":
            renderIcon = <PersonAdd />;
            break;
        case "Home":
            renderIcon = <Home />;
            break;
        case "ViewList":
            renderIcon = <ViewList />;
            break;
        case "Subject":
            renderIcon = <Subject />;
            break;
        case "Build":
            renderIcon = <Build />;
            break;
        case "Assignment":
            renderIcon = <Assignment />;
            break;
        case "Star":
            renderIcon = <Star />;
            break;
        case "PhotoCamera":
            renderIcon = <PhotoCamera />;
            break;
        case "SportsSoccer":
            renderIcon = <SportsSoccer />;
            break;
        case "MusicNote":
            renderIcon = <MusicNote />;
            break;
        case "DirectionsBike":
            renderIcon = <DirectionsBike />;
            break;
        case "ContactMail":
            renderIcon = <ContactMail />;
            break;
        case "Gavel":
            renderIcon = <Gavel />;
            break;
        case "Policy":
            renderIcon = <Policy />;
            break;
        default: renderIcon = <Apple />;
    }

    return(<>{renderIcon}</>);
}
