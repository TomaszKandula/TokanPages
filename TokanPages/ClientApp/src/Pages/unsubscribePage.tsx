import * as React from "react";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import Footer from "../Components/Layout/footer";
import Unsubscribe from "../Components/Unsubscribe/unsubscribe";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

export default function UnsubscribePage()
{
    const queryParam = useQuery();
    const id = queryParam.get("id");

    return(
        <>
            <Navigation content={null} />
            <Container>
                <Unsubscribe id={id} />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );
}
