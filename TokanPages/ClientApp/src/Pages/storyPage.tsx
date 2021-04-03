import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import StaticContent from "../Components/Content/staticContent";
import Footer from "../Components/Layout/footer";
import * as Consts from "../Shared/constants";
import { footerContentDefault, navigationContentDefault } from "../Api/Defaults";
import { getFooterContent, getNavigationContent } from "../Api/Services";

export default function StoryPage() 
{ 
    const mountedRef = React.useRef(true);
    const [navigation, setNavigationContent] = React.useState({ data: navigationContentDefault, isLoading: true });
    const [footer, setFooterContent] = React.useState({ data: footerContentDefault, isLoading: true });

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setNavigationContent({ data: await getNavigationContent(), isLoading: false });
        setFooterContent({ data: await getFooterContent(), isLoading: false });
    }, [ ]);

    React.useEffect(() => 
    {
        updateContent();
        return () => { mountedRef.current = false; };
    }, 
    [ updateContent ]);

    return (
        <>
            <Navigation navigation={navigation.data} isLoading={navigation.isLoading} />
            <Container>
                <StaticContent dataUrl={Consts.STORY_URL} />
            </Container>
            <Footer footer={footer.data} isLoading={footer.isLoading} />
        </>
    );
}
