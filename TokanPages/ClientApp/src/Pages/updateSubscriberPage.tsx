import * as React from "react";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import Footer from "../Components/Layout/footer";
import UpdateSubscriber from "Components/UpdateSubscription/updateSubscriber";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

export default function UpdateSubscriberPage()
{
    const queryParam = useQuery();
    const id = queryParam.get("id");

    return(
        <>
            <Navigation content={null} />
            <Container>
                <UpdateSubscriber id={id} />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );
}
