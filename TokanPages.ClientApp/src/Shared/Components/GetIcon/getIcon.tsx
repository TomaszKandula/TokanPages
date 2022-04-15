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
    ViewList, 
    VpnKey,
    Lock,
    Code,
    GitHub,
    MenuBook,
    Edit,
} from "@material-ui/icons";

interface IProperty
{
    iconName: string;
}

export const GetIcon = (props: IProperty): JSX.Element =>
{
    switch(props.iconName)
    {
        case "Person": return <Person />;
        case "PersonAdd": return <PersonAdd />;
        case "Home": return <Home />;
        case "ViewList": return <ViewList />;
        case "Subject": return <Subject />;
        case "Build": return <Build />;
        case "Assignment": return <Assignment />;
        case "Star": return <Star />;
        case "PhotoCamera": return <PhotoCamera />;
        case "SportsSoccer": return <SportsSoccer />;
        case "MusicNote": return <MusicNote />;
        case "DirectionsBike": return <DirectionsBike />;
        case "ContactMail": return <ContactMail />;
        case "Gavel": return <Gavel />;
        case "Policy": return <Policy />;
        case "VpnKey": return <VpnKey />;
        case "Lock": return <Lock />; 
        case "Code": return <Code />; 
        case "GitHub": return <GitHub />; 
        case "MenuBook": return <MenuBook />; 
        case "Edit": return <Edit />; 

        default: return <Apple />;
    }
}
