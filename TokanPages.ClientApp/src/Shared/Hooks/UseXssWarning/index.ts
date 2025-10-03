import * as React from "react";
import { useSelector } from "react-redux";
import { WarningModelDto } from "../../../Api/Models";
import { ApplicationState } from "../../../Store/Configuration";

const selfXssWarnignConfig = {
    banner: {
        color: "#ec0000",
        fontWeight: "bold",
        fontSize: "35px",
    },
    secondLine: {
        fontWeight: "bold",
        fontSize: "15px",
    },
};

export const useXssWarning = (): void => {
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        const languageId = language?.id;
        const warnings = language?.warnings ?? [];
        if (warnings.length === 0) {
            return;
        }

        const filtering = (value: WarningModelDto): boolean => value.language === languageId;
        const data = warnings.filter(filtering);
        if (data.length !== 1) {
            return;
        }

        const warning = data[0];
        console.log(
            "%c" + warning.caption,
            "color:" +
                selfXssWarnignConfig.banner.color +
                "; font-weight:" +
                selfXssWarnignConfig.banner.fontWeight +
                ";" +
                "font-size:" +
                selfXssWarnignConfig.banner.fontSize +
                ";"
        );
        console.log(warning.text1);
        console.log(
            "%c" + warning.text2,
            "font-weight:" +
                selfXssWarnignConfig.secondLine.fontWeight +
                ";" +
                "font-size:" +
                selfXssWarnignConfig.secondLine.fontSize +
                ";"
        );
        console.log(warning.text3);
    }, [language]);
};
