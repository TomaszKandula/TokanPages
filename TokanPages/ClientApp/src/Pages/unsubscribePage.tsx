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
    const id = queryParam.get("id") as string;

    const mountedRef = React.useRef(true);
    const [unsubscribe, SetUnsubscribePageContent] = React.useState({ data: unsubscribeDefault, isLoading: true });
    const [navigation, setNavigationContent] = React.useState({ data: navigationDefault, isLoading: true });
    const [footer, setFooterContent] = React.useState({ data: footerDefault, isLoading: true });

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        SetUnsubscribePageContent({ data: await getUnsubscribeContent(), isLoading: false });
        setNavigationContent({ data: await getNavigationContent(), isLoading: false });
        setFooterContent({ data: await getFooterContent(), isLoading: false });
    }, [ ]);

    React.useEffect(() => 
    {
        updateContent();
        return () => { mountedRef.current = false; };
    }, [ updateContent ]);

    return(
        <>
            <Navigation navigation={navigation.data} isLoading={navigation.isLoading} />
            <Container>
                <Unsubscribe id={id} unsubscribe={unsubscribe.data} isLoading={unsubscribe.isLoading} />
            </Container>
            <Footer footer={footer.data} isLoading={footer.isLoading} />
        </>
    );
}
