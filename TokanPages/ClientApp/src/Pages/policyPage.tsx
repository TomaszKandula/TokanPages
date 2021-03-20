import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import StaticContent from "../Components/Content/staticContent";
import Footer from "../Components/Layout/footer";
import { POLICY_URL } from "../Shared/constants";
import { CustomColours } from "../Theme/customColours";
import { footerDefault, navigationDefault } from "../Api/Defaults";
import { getNavigationContent, getFooterContent } from "../Api/Services";

export default function PolicyPage() 
{
    const mountedRef = React.useRef(true);
    const [navigation, setNavigationContent] = React.useState({ data: navigationDefault, isLoading: true });
    const [footer, setFooterContent] = React.useState({ data: footerDefault, isLoading: true });

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
                <StaticContent dataUrl={POLICY_URL} />
            </Container>
            <Footer footer={footer.data} isLoading={footer.isLoading} backgroundColor={CustomColours.background.gray1} />
        </>
    );
}
