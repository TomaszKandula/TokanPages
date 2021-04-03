import * as React from "react";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import Footer from "../Components/Layout/footer";
import UpdateSubscriber from "../Components/UpdateSubscription/updateSubscriber";
import { footerContentDefault, navigationContentDefault, updateSubscriberContentDefault } from "../Api/Defaults";
import { getFooterContent, getNavigationContent, getUpdateSubscriberContent } from "../Api/Services";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

export default function UpdateSubscriberPage()
{
    const queryParam = useQuery();
    const id = queryParam.get("id") as string; 

    const mountedRef = React.useRef(true);
    const [updateSubscriber, setUpdateSubscriberContent] = React.useState({ data: updateSubscriberContentDefault, isLoading: true });
    const [navigation, setNavigationContent] = React.useState({ data: navigationContentDefault, isLoading: true });
    const [footer, setFooterContent] = React.useState({ data: footerContentDefault, isLoading: true });

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setUpdateSubscriberContent({ data: await getUpdateSubscriberContent(), isLoading: false });
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
                <UpdateSubscriber id={id} updateSubscriber={updateSubscriber.data} isLoading={updateSubscriber.isLoading} />
            </Container>
            <Footer footer={footer.data} isLoading={footer.isLoading} />
        </>
    );
}
