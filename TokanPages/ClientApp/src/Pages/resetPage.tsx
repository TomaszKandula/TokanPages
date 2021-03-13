import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import ResetForm from "../Components/Account/resetForm";
import Footer from "../Components/Layout/footer";
import { resetFormDefault } from "../Api/Defaults";
import { getResetFormText } from "../Api/Services";

export default function ResetPage() 
{
    const mountedRef = React.useRef(true);
    const [resetForm, setResetForm] = React.useState(resetFormDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setResetForm(await getResetFormText());
    }, [ ]);

    React.useEffect(() => 
    {
        updateContent();
        return () => { mountedRef.current = false; };
    }, [ updateContent ]);

    return (
        <>     
            <Navigation content={null} />
            <Container>
                <ResetForm content={resetForm.content} />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );
}
