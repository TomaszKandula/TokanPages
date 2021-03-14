import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import StaticContent from "../Components/Content/staticContent";
import Footer from "../Components/Layout/footer";
import * as Consts from "../Shared/constants";
import { navigationDefault } from "../Api/Defaults";
import { getNavigationContent } from "../Api/Services";

export default function StoryPage() 
{ 
    const mountedRef = React.useRef(true);
    const [navigation, setNavigationContent] = React.useState(navigationDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setNavigationContent(await getNavigationContent());
    }, [ ]);

    React.useEffect(() => 
    {
        updateContent();
        return () => { mountedRef.current = false; };
    }, 
    [ updateContent ]);

    return (
        <>
            <Navigation content={navigation.content} />
            <Container>
                <StaticContent dataUrl={Consts.STORY_URL} />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );
}
