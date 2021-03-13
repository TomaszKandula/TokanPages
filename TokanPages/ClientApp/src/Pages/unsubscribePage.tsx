import * as React from "react";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import Footer from "../Components/Layout/footer";
import Unsubscribe from "../Components/Unsubscribe/unsubscribe";
import { unsubscribeDefault } from "../Api/Defaults";
import { getUnsubscribeText } from "../Api/Services";

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

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        SetUnsubscribePage(await getUnsubscribeText());
    }, [ ]);

    React.useEffect(() => 
    {
        updateContent();
        return () => { mountedRef.current = false; };
    }, [ updateContent ]);

    return(
        <>
            <Navigation content={null} />
            <Container>
                <Unsubscribe {...id} content={unsubscribePage.content} />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );
}
