import os
import torchvision.models as models
import torch
import pathlib


class IA:
    path = pathlib.Path(__file__).parent.resolve().__str__() + "\\model"
    trainable = True

    def __init__(self):
        
        torch.backends.cudnn.benchmark = True
        self.device = "cuda" if torch.cuda.is_available() else "cpu"
        # self.model = models.resnet50(weights=None, num_classes=3).to(self.device)

        # self.model = models.MobileNetV2(num_classes=3).to(self.device)

        self.model = models.resnet50(weights=models.ResNet50_Weights.DEFAULT).to(self.device)
        params = self.model.parameters()

        for param in params :
            param.requires_grad = False
        for param in self.model.fc.parameters():
            param.requires_grad = True
        for param in self.model.layer4.parameters():
            param.requires_grad = True




        if os.listdir(self.path) :
            self.model.load_state_dict(torch.load(self.path + "\\model.pth", weights_only=True))
            self.trainable = False

        self.criterion = torch.nn.CrossEntropyLoss()
        self.optimizer = torch.optim.Adam(self.model.parameters(), lr=0.001)


    def train(self,x, y):
        self.optimizer.zero_grad()
        self.model.train()
        y_pred = self.model(x)
        loss = self.criterion(y_pred, y)
        loss.backward()
        self.optimizer.step()
    
        return loss.item()

    def prediction(self, loader):
        total_correct = 0
        total_samples = 0
        with torch.no_grad():
            for x, y in loader:
                x = x.to(self.device)
                y = y.to(self.device)

                self.model.eval()
                y_pred = self.model(x)
                _, predictions = torch.max(y_pred, 1)

                total_correct += (predictions == y).sum().item()
                total_samples += y.size(0)


            accuracy = 100 * total_correct / total_samples
            return accuracy




