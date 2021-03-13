import * as React from "react";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import Footer from "../Components/Layout/footer";
import Unsubscribe from "../Components/Unsubscribe/unsubscribe";
import { navigationDefault, unsubscribeDefault } from "../Api/Defaults";
import { getNavigationText, getUnsubscribeText } from "../Api/Services";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

export default function UnsubscribePage()
{
    const queryParam = useQuery();
    const id = queryParam.get("id");

    const mountedRef = React.useRef(true);
    const [unsubscribePage, SetUnsubscribePage] = React.useState(unsubscribeDefault);
    const [navigation, setNavigation] = React.useState(navigationDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        SetUnsubscribePage(await getUnsubscribeText());
        setNavigation(await getNavigationText());
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
            <Footer backgroundColor="#FAFAFA" />
        </>
    );
}
