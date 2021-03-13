import * as React from "react";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import Footer from "../Components/Layout/footer";
import UpdateSubscriber from "Components/UpdateSubscription/updateSubscriber";
import { updateSubscriberDefault } from "../Api/Defaults";
import { getUpdateSubscriberText } from "../Api/Services";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

export default function UpdateSubscriberPage()
{
    const queryParam = useQuery();
    const id = queryParam.get("id");

    const mountedRef = React.useRef(true);
    const [updateSubscriberPage, setUpdateSubscriberPage] = React.useState(updateSubscriberDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setUpdateSubscriberPage(await getUpdateSubscriberText());
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
                <UpdateSubscriber {...id} content={updateSubscriberPage.content} />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );
}
