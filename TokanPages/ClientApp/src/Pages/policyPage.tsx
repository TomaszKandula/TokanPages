import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import StaticContent from "../Components/Content/staticContent";
import Footer from "../Components/Layout/footer";
import { POLICY_URL } from "../Shared/constants";
import { footerDefault, navigationDefault } from "../Api/Defaults";
import { getNavigationContent, getFooterContent } from "../Api/Services";

export default function PolicyPage() 
{
    const mountedRef = React.useRef(true);
    const [navigation, setNavigationContent] = React.useState(navigationDefault);
    const [footer, setFooterContent] = React.useState(footerDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setNavigationContent(await getNavigationContent());
        setFooterContent(await getFooterContent());
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
                <StaticContent dataUrl={POLICY_URL} />
            </Container>
            <Footer footer={footer} backgroundColor="#FAFAFA" />
        </>
    );
}
