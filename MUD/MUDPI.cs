using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDEE {
	class MUDPI {
		/* ***Multi-User Domain Protocol Interface***
		 * MUDPI is very much a client-server protocol, unlike Telnet.
		 * It operates via escape sequences initiated by BMC (0x91) and terminated by EMC (0x92).
		 * BMC and EMC are the two C1-block Private Use control characters, as per ECMA-48.
		 * Commands
		 */

		public enum CommandCode {
			BMC = 0x91, //Begin MUDPI Command
			EMC = 0x92, //End MUDPI Command

		}
	}
}
