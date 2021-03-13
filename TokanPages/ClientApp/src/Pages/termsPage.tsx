import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import StaticContent from "../Components/Content/staticContent";
import Footer from "../Components/Layout/footer";
import * as Consts from "../Shared/constants";
import { navigationDefault } from "../Api/Defaults";
import { getNavigationText } from "../Api/Services";

export default function TermsPage() 
{
    const mountedRef = React.useRef(true);
    const [navigation, setNavigation] = React.useState(navigationDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setNavigation(await getNavigationText());
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
                <StaticContent dataUrl={Consts.TERMS_URL} />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );
}
