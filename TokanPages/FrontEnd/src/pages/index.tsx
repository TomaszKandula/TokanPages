import React from "react";

import HorizontalNav from "../components/horizontal-navs/HorizontalNav";
import StructureContainer from "../components/structures/StructureContainer";
import StructureTwoColumns from "../components/structures/StructureTwoColumns";
import Headers from "../components/Headers";
import Elements from "../components/Elements";
import Features from "../components/Features";
import Featured from "../components/Featured";
import Contacts from "../components/Contacts";
import Footers from "../components/Footers";

export default function Index() 
{

  return (
    <React.Fragment>
      <HorizontalNav content={null} />

      <StructureContainer
        columns={[
          <StructureTwoColumns
            column1={[<Headers />]}
            column2={[<Elements />]}
          />,
          <Features />,
          <Featured />,
          <Contacts />,
          <Footers />,
        ]}
      />

    </React.Fragment>
  );

}
