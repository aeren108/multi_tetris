using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace MultiTetris.Net {
    class Client {
        private NetClient client;
        private NetPeerConfiguration config;

        private NetOutgoingMessage outMsg;
        private NetIncomingMessage inMsg;

        private string host;
        private int port;
        public Client(string host, int port) {
            config = new NetPeerConfiguration("Tetris");
            client = new NetClient(config);

            this.host = host;
            this.port = port;
        }

        public void Connect() {
            outMsg = client.CreateMessage();
            outMsg.Write((byte) PacketTypes.LOGIN);

            client.Connect(host, port, outMsg);
        }

        public void Update() {
            if ((inMsg = client.ReadMessage()) != null) {
                switch (inMsg.MessageType) {
                    case NetIncomingMessageType.Data:
                        if (inMsg.ReadByte() == (byte) PacketTypes.ARENA_STATE) {

                        } else if (inMsg.ReadByte() == (byte) PacketTypes.TETROMINO_STATE) {

                        }
                        break;
                }
            }
        }

        private void WaitApproval() {
            bool isApproved = false;

            while (!isApproved) {
                if ((inMsg = client.ReadMessage()) != null) {
                    switch (inMsg.MessageType) {
                        case NetIncomingMessageType.Data:
                            if (inMsg.ReadByte() == (byte) PacketTypes.TETROMINO_STATE) {
                                isApproved = true;
                            }
                            break;
                    }
                }
            }
        }

    }

    enum PacketTypes {
        LOGIN,
        TETROMINO_STATE,
        ARENA_STATE

    }
}
