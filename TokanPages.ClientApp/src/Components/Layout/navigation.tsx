import * as React from "react";
import { IGetNavigationContent } from "../../Redux/States/getNavigationContentState";
import NavigationView from "./navigationView";

export default function Navigation(props: IGetNavigationContent) 
{
    const content = 
    {
        logo: props.content.logo,
        login: 'Login',
        register: 'Register',
        home: 'Home',
        articles: 'Articles',
        story: 'My story',
        projects: 'My projects',
        interests: 'My interests',
        orion: 'VAT Validation',
        photography: 'Photography',
        football: 'Football',
        guitar: 'Guitar',
        bicycle: 'Bicycle',
        diy: 'DIY',
        contact: 'Contact',
        terms: 'Terms of use',
        policy:'Privacy policy',
        avatar: 'https://maindbstorage.blob.core.windows.net/tokanpages/content/avatars/avatar-default-288.jpeg',
        material: 'https://maindbstorage.blob.core.windows.net/tokanpages/content/images/material-background-1.jpg'
    };
    
    const [drawer, setDrawer] = React.useState({ open: false});
    const toggleDrawer = (open: boolean) => (event: any) => 
    {
        if (event.type === "keydown" && (event.key === "Tab" || event.key === "Shift")) return;
        setDrawer({ ...drawer, open });
    };

    const [projects, setProjects] = React.useState(false);
    const handleProjectsClick = () => setProjects(!projects);

    const [interests, setInterests] = React.useState(false);
    const handleInterestsClick = () => setInterests(!interests);

    // TODO: use user service provider
    const userAlias = "Dummy";
    const anonymous = "Anonymous";
    const isAnonymous = true; 

    return (<NavigationView bind=
    {{  
        drawerState: drawer,
        openHandler: toggleDrawer(true),
        closeHandler: toggleDrawer(false),
        projectsHandler: handleProjectsClick,
        projectsState: projects,
        interestsHandler: handleInterestsClick,
        interestState: interests,
        isAnonymous: isAnonymous,
        anonymous: anonymous,
        userAlias: userAlias,
        content: content
    }}/>);
}
