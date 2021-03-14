import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import ResetForm from "../Components/Account/resetForm";
import Footer from "../Components/Layout/footer";
import { navigationDefault, resetFormDefault } from "../Api/Defaults";
import { getNavigationContent, getResetFormContent } from "../Api/Services";

export default function ResetPage() 
{
    const mountedRef = React.useRef(true);
    const [resetForm, setResetFormContent] = React.useState(resetFormDefault);
    const [navigation, setNavigationContent] = React.useState(navigationDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setResetFormContent(await getResetFormContent());
        setNavigationContent(await getNavigationContent());
    }, [ ]);

    React.useEffect(() => 
    {
        updateContent();
        return () => { mountedRef.current = false; };
    }, [ updateContent ]);

    return (
        <>     
            <Navigation content={navigation.content} />
            <Container>
                <ResetForm content={resetForm.content} />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );
}
