import pathlib
import sys
import torchvision.transforms as transforms
import torch
import torchvision
import torch.utils as utils
import tqdm
import IA
from PIL import Image

device = "cuda" if torch.cuda.is_available() else "cpu"
ia = IA.IA()
transform = transforms.Compose([transforms.Resize((224,224)), transforms.ToTensor()])
path = pathlib.Path(__file__).parent.resolve().__str__() + "\\assets"
training_set = torchvision.datasets.ImageFolder(root=path + "\\train", transform=transform)
if ia.trainable == True :

    validate_set = torchvision.datasets.ImageFolder(root=path + "\\validate", transform=transform)
    testing_set = torchvision.datasets.ImageFolder(root=path + "\\test", transform=transform)

    train_loader = utils.data.DataLoader(training_set, batch_size=32, shuffle=True)
    validate_loader = utils.data.DataLoader(validate_set, batch_size=32, shuffle=True)
    test_loader = utils.data.DataLoader(testing_set, batch_size=32, shuffle=True)

    epochs = 20

    for epoch in range(epochs) :
    
        for x_train, y_train in tqdm.tqdm(train_loader):
            x_train = x_train.to(device)
            y_train = y_train.to(device)
            loss = ia.train(x_train, y_train)

        print(ia.prediction(validate_loader))
            
    print(ia.prediction(test_loader))
    torch.save(ia.model.state_dict(), ia.path + "\\model.pth")

print("ready")

for line in sys.stdin:
    imagePath = line.strip()
    image = Image.open(imagePath)
    image = image.convert("RGB")
    x = transform(image).unsqueeze(0)
    with torch.no_grad():
        ia.model.eval()
        y_pred = ia.model(x)
    _, predictions = torch.max(y_pred, 1)
    print(training_set.classes[predictions.item()])
    

