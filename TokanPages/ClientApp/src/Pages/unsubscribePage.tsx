import * as React from "react";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import Footer from "../Components/Layout/footer";
import Unsubscribe from "../Components/Unsubscribe/unsubscribe";
import { footerDefault, navigationDefault, unsubscribeDefault } from "../Api/Defaults";
import { getFooterContent, getNavigationContent, getUnsubscribeContent } from "../Api/Services";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

export default function UnsubscribePage()
{
    const queryParam = useQuery();
    const id = queryParam.get("id");

    const mountedRef = React.useRef(true);
    const [unsubscribePage, SetUnsubscribePageContent] = React.useState(unsubscribeDefault);
    const [navigation, setNavigationContent] = React.useState(navigationDefault);
    const [footer, setFooterContent] = React.useState(footerDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        SetUnsubscribePageContent(await getUnsubscribeContent());
        setNavigationContent(await getNavigationContent());
        setFooterContent(await getFooterContent());
    }, [ ]);

    React.useEffect(() => 
    {
        updateContent();
        return () => { mountedRef.current = false; };
    }, [ updateContent ]);

    return(
        <>
            <Navigation content={navigation.content} />
            <Container>
                <Unsubscribe {...id} content={unsubscribePage.content} />
            </Container>
            <Footer footer={footer} backgroundColor="#FAFAFA" />
        </>
    );
}
