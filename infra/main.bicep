param projectName string 

param openaiAccountName string
param speechAccountName string
param visionAccountName string

param openaiDeploymentName string = 'gpt-4o'
param openaiDeploymentModel string = 'gpt-4o'
param filterPolicyName string = 'MockingMirrorFilter'

param location string = resourceGroup().location

resource openaiAccount 'Microsoft.CognitiveServices/accounts@2024-10-01' = {
  name: openaiAccountName
  location: location
  sku: {
    name: 'S0'
  }
  kind: 'OpenAI'
  tags: {
    Project: projectName
  }
  properties: {
    apiProperties: {}
    customSubDomainName: openaiAccountName
    networkAcls: {
      defaultAction: 'Allow'
      virtualNetworkRules: []
      ipRules: []
    }
    publicNetworkAccess: 'Enabled'
  }
}

resource speechAccount 'Microsoft.CognitiveServices/accounts@2024-10-01' = {
  name: speechAccountName
  location: location
  sku: {
    name: 'F0'
  }
  kind: 'SpeechServices'
  identity: {
    type: 'None'
  }
  tags: {
    Project: projectName
  }
  properties: {
    networkAcls: {
      defaultAction: 'Allow'
      virtualNetworkRules: []
      ipRules: []
    }
    publicNetworkAccess: 'Enabled'
  }
}

resource visionAccount 'Microsoft.CognitiveServices/accounts@2024-10-01' = {
  name: visionAccountName
  location: 'eastus'
  sku: {
    name: 'F0'
  }
  kind: 'ComputerVision'
  identity: {
    type: 'None'
  }
  tags: {
    Project: projectName
  }
  properties: {
    customSubDomainName: visionAccountName
    networkAcls: {
      defaultAction: 'Allow'
      virtualNetworkRules: []
      ipRules: []
    }
    publicNetworkAccess: 'Enabled'
  }
}

resource openaiDeployment 'Microsoft.CognitiveServices/accounts/deployments@2024-10-01' = {
  parent: openaiAccount
  name: openaiDeploymentName
  sku: {
    name: 'GlobalStandard'
    capacity: 10
  }
  properties: {
    model: {
      format: 'OpenAI'
      name: openaiDeploymentModel
    }
    versionUpgradeOption: 'OnceNewDefaultVersionAvailable'
    currentCapacity: 10
    raiPolicyName: filterPolicyName
  }
}

resource openaiFilterPolicy 'Microsoft.CognitiveServices/accounts/raiPolicies@2024-10-01' = {
  parent: openaiAccount
  name: filterPolicyName
  properties: {
    mode: 'Default'
    basePolicyName: 'Microsoft.DefaultV2'
    contentFilters: [
      {
        name: 'Violence'
        severityThreshold: 'High'
        blocking: true
        enabled: true
        source: 'Prompt'
      }
      {
        name: 'Hate'
        severityThreshold: 'High'
        blocking: true
        enabled: true
        source: 'Prompt'
      }
      {
        name: 'Sexual'
        severityThreshold: 'Medium'
        blocking: true
        enabled: true
        source: 'Prompt'
      }
      {
        name: 'Selfharm'
        severityThreshold: 'High'
        blocking: true
        enabled: true
        source: 'Prompt'
      }
      {
        name: 'Jailbreak'
        blocking: true
        enabled: true
        source: 'Prompt'
      }
      {
        name: 'Indirect Attack'
        blocking: false
        enabled: false
        source: 'Prompt'
      }
      {
        name: 'Violence'
        severityThreshold: 'High'
        blocking: true
        enabled: true
        source: 'Completion'
      }
      {
        name: 'Hate'
        severityThreshold: 'High'
        blocking: true
        enabled: true
        source: 'Completion'
      }
      {
        name: 'Sexual'
        severityThreshold: 'Medium'
        blocking: true
        enabled: true
        source: 'Completion'
      }
      {
        name: 'Selfharm'
        severityThreshold: 'High'
        blocking: true
        enabled: true
        source: 'Completion'
      }
      {
        name: 'Protected Material Text'
        blocking: true
        enabled: true
        source: 'Completion'
      }
      {
        name: 'Protected Material Code'
        blocking: false
        enabled: true
        source: 'Completion'
      }
    ]
  }
}

resource openaiDefender 'Microsoft.CognitiveServices/accounts/defenderForAISettings@2024-10-01' = {
  parent: openaiAccount
  name: 'Default'
  properties: {
    state: 'Disabled'
  }
}
